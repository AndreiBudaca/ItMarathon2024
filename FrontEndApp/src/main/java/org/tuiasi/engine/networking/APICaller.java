package org.tuiasi.engine.networking;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import javax.net.ssl.SSLContext;
import javax.net.ssl.TrustManager;
import javax.net.ssl.X509TrustManager;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.security.cert.X509Certificate;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;

public class APICaller {

    HttpClient client;
    public static String current_jwt;

    public APICaller(){
        // Create a trust manager that trusts all certificates
        TrustManager[] trustAllCerts = new TrustManager[]{
                new X509TrustManager() {
                    public java.security.cert.X509Certificate[] getAcceptedIssuers() {
                        return new X509Certificate[0];
                    }

                    public void checkClientTrusted(
                            java.security.cert.X509Certificate[] certs, String authType) {
                    }

                    public void checkServerTrusted(
                            java.security.cert.X509Certificate[] certs, String authType) {
                    }
                }
        };

        // Create SSLContext with the trust manager that trusts all certificates
        SSLContext sslContext = null;
        try {
            sslContext = SSLContext.getInstance("SSL");
            sslContext.init(null, trustAllCerts, new java.security.SecureRandom());
        } catch (Exception e) {
            throw new RuntimeException(e);
        }


        // Create HttpClient with the custom SSLContext
        client = HttpClient.newBuilder()
                .sslContext(sslContext)
                .build();
    }

    public void testFunc(){
        try {
            HttpRequest request = HttpRequest.newBuilder()
                    .uri(new URI("https://localhost:7262/HelloWorld" + "?echo=John"))
                    .header("accept", "text/plain")
                    .GET()
                    .build();

            client.sendAsync(request, HttpResponse.BodyHandlers.ofString())
                    .thenApply(HttpResponse::body)
                    .thenAccept(System.out::println)
                    .join();

        } catch (Exception e) {
            throw new RuntimeException(e);
        }

    }

    public boolean login(LoginDTO loginDTO){
        try {
            JSONObject obj = new JSONObject();
            obj.put("email", loginDTO.email);
            obj.put("password", loginDTO.password);

            System.out.println(obj.toJSONString());

            HttpRequest request = HttpRequest.newBuilder()
                    .POST(HttpRequest.BodyPublishers.ofString(obj.toJSONString()))
                    .uri(new URI("https://localhost:7262/api/Authentication/Login"))
                    .header("accept", "application/json")
                    .header("content-type", "application/json")
                    .build();

            client.sendAsync(request, HttpResponse.BodyHandlers.ofString())
                    .thenApply(HttpResponse::body)
                    .thenAccept(response -> {
                        current_jwt = response.substring(1, response.length()-1);
                    })
                    .join();

            return !current_jwt.equals("The email / password combination is invalid");

        } catch (Exception e) {
            throw new RuntimeException(e);
        }

    }

    public boolean register(RegisterDTO registerDTO){
        try {
            JSONObject obj = new JSONObject();
            obj.put("email", registerDTO.email);
            obj.put("password", registerDTO.password);
            obj.put("yearOfStudy", Integer.valueOf(registerDTO.yearOfStudy));
            obj.put("firstName", registerDTO.firstName);
            obj.put("lastName", registerDTO.lastName);

            System.out.println(obj.toJSONString());

            HttpRequest request = HttpRequest.newBuilder()
                    .POST(HttpRequest.BodyPublishers.ofString(obj.toJSONString()))
                    .uri(new URI("https://localhost:7262/api/Authentication/Register"))
                    .header("accept", "application/json")
                    .header("content-type", "application/json")
                    .build();

            return client.sendAsync(request, HttpResponse.BodyHandlers.ofString())
                    .thenApply(HttpResponse::statusCode)
                    .join() == 200;

        } catch (Exception e) {
            throw new RuntimeException(e);
        }

    }

    public JSONArray getOptionalPacks(){
        try {
            JSONParser parser = new JSONParser();
            JSONArray obj;

            HttpRequest request = HttpRequest.newBuilder()
                    .GET()
                    .uri(new URI("https://localhost:7262/api/StudentOptionalPreferences"))
                    .header("accept", "*/*")
                    .header("content-type", "*/*")
                    .header("Authorization", "Bearer " + current_jwt)
                    .build();

            System.out.println(current_jwt);
            HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());

            String responseBody = response.body();
            obj = (JSONArray) parser.parse(responseBody);
            return obj;

        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    public boolean saveOptionals(List<SubjectPreferenceDTO> preference_order_pair){
        try{
            JSONArray array = new JSONArray();

            for(SubjectPreferenceDTO pair: preference_order_pair){
                if(pair.getOptionalId() != null) {
                    JSONObject obj = new JSONObject();
                    obj.put("optionalId", pair.getOptionalId());
                    obj.put("sortOrder", pair.getSortOrder());
                    array.add(obj);
                }
            }

            System.out.println(array.toJSONString());

            HttpRequest request = HttpRequest.newBuilder()
                    .POST(HttpRequest.BodyPublishers.ofString(array.toJSONString()))
                    .uri(new URI("https://localhost:7262/api/StudentOptionalPreferences"))
                    .header("accept", "*/*")
                    .header("content-type", "application/json")
                    .header("Authorization", "Bearer " + current_jwt)
                    .build();

            System.out.println(current_jwt);
            HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());

            return response.statusCode() == 200;

        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

}
