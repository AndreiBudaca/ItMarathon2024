package org.tuiasi.engine.ui.uiWindows.prefabs;

import imgui.ImGui;
import imgui.ImVec2;
import imgui.flag.ImGuiDir;
import lombok.Getter;
import lombok.Setter;
import org.tuiasi.engine.networking.APICaller;
import org.tuiasi.engine.networking.LoginDTO;
import org.tuiasi.engine.networking.RegisterDTO;
import org.tuiasi.engine.ui.AppWindow;
import org.tuiasi.engine.ui.DefaultAppUI;
import org.tuiasi.engine.ui.components.IComponent;
import org.tuiasi.engine.ui.components.basicComponents.button.Button;
import org.tuiasi.engine.ui.components.basicComponents.button.ButtonListener;
import org.tuiasi.engine.ui.components.basicComponents.checkbox.CheckboxWithTitle;
import org.tuiasi.engine.ui.components.basicComponents.dropdown.DropdownWithTitle;
import org.tuiasi.engine.ui.components.basicComponents.label.Label;
import org.tuiasi.engine.ui.components.basicComponents.searchbar.SearchbarWithHint;
import org.tuiasi.engine.ui.uiWindows.Page;
import org.tuiasi.engine.ui.uiWindows.UIWindow;

import java.util.ArrayList;
import java.util.Base64;
import java.util.List;

public class LoginWindow extends UIWindow {

    @Getter @Setter
    private String mode = "Login";

    public LoginWindow(String windowTitle) {
        super(windowTitle);
    }

    public LoginWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size) {
        super(windowTitle, relativePosition, size);
    }

    public LoginWindow(String windowTitle, int dockDirection, float dockRatio) {
        super(windowTitle, dockDirection, dockRatio);
    }

    public LoginWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size, boolean isRootWindow) {
        super(windowTitle, relativePosition, size, isRootWindow);
    }

    public LoginWindow(String windowTitle, int dockDirection, float dockRatio, boolean isRootWindow) {
        super(windowTitle, dockDirection, dockRatio, isRootWindow);
        addPrefabComponents();
    }

    @Override
    protected void addPrefabComponents(){
        configurePage(mode);
    }

    private void updatePage(){
        clearComponents();
        mode = mode.equals("Login") ? "Register" : "Login";
        configurePage(mode);
    }

    private void configurePage(String mode){

        Label loginLabel = new Label("Welcome!", false, 32);
        loginLabel.setRatioedPosition(0.5f, 0.2f);
        addComponent(loginLabel);

        SearchbarWithHint emailInput = new SearchbarWithHint("Email", "Email", false);
        emailInput.setSize(300, 50);
        emailInput.setRatioedPosition(0.5f, 0.3f);
        addComponent(emailInput);

        SearchbarWithHint passwordInput = new SearchbarWithHint("Password", "Password", true);
        passwordInput.setSize(300, 50);
        passwordInput.setRatioedPosition(0.5f,  0.33f);
        addComponent(passwordInput);

        SearchbarWithHint numeInput = new SearchbarWithHint();
        SearchbarWithHint prenumeInput = new SearchbarWithHint();
        DropdownWithTitle anInput = new DropdownWithTitle();

        if(mode.equals("Register")){
            numeInput = new SearchbarWithHint("Nume", "Nume", false);
            numeInput.setSize(300, 50);
            numeInput.setRatioedPosition(0.5f, 0.36f);
            addComponent(numeInput);

            prenumeInput = new SearchbarWithHint("Prenume", "Prenume", false);
            prenumeInput.setSize(300, 50);
            prenumeInput.setRatioedPosition(0.5f, 0.39f);
            addComponent(prenumeInput);

            anInput = new DropdownWithTitle("An studiu", new String[]{"1", "2", "3"});
            anInput.setSize(100, 50);
            anInput.setRatioedPosition(0.5f, 0.42f);
            addComponent(anInput);
        }

        Label actionStatusLabel = new Label("");
        actionStatusLabel.setSize(loginLabel.getWidth() + 100,  50);
        actionStatusLabel.setRatioedPosition(0.5f,  mode.equals("Register") ? 0.50f : 0.42f);
        addComponent(actionStatusLabel);

        Button loginButton = new Button(mode);
        loginButton.setSize(loginLabel.getWidth() + 100,  50);
        loginButton.setRatioedPosition(0.5f,  mode.equals("Register") ? 0.45f : 0.37f);
        SearchbarWithHint finalNumeInput = numeInput;
        SearchbarWithHint finalPrenumeInput = prenumeInput;
        DropdownWithTitle finalAnInput = anInput;
        loginButton.setListener(new ButtonListener() {
            @Override
            public void onClick() {
                APICaller apiCaller = new APICaller();
                if(mode.equals("Login")) {
                    boolean loginStatus = apiCaller.login(new LoginDTO(emailInput.getSearchText().get(), passwordInput.getSearchText().get()));
                    if(!loginStatus)
                        actionStatusLabel.setLabel("Logarea a esuat. Verifica credentialele");
                    else {
                        String[] chunks = APICaller.current_jwt.split("\\.");
                        Base64.Decoder decoder = Base64.getUrlDecoder();

//                        String header = new String(decoder.decode(chunks[0]));
                        String payload = new String(decoder.decode(chunks[1]));

                        if(payload.contains("Student")) {
                            // the student home page
                            Page studentHomePage = new Page("StudentHomePage",
                                    List.of(
                                            new StudentHomeWindow("StudentHomeWindow", ImGuiDir.None, 1.0f, true)
                                    )
                            );
                            DefaultAppUI.addPage(studentHomePage.getName(), studentHomePage);
                            DefaultAppUI.setCurrentPage("StudentHomePage");
                        }
                        else{
                            // the student home page
                            Page adminHomePage = new Page("AdminHomePage",
                                    List.of(
                                            new AdminHomeWindow("AdminHomeWindow", ImGuiDir.None, 1.0f, true)
                                    )
                            );
                            DefaultAppUI.addPage(adminHomePage.getName(), adminHomePage);
                            DefaultAppUI.setCurrentPage("AdminHomePage");

                        }
                    }
                }
                else {
                    boolean registerStatus = apiCaller.register(new RegisterDTO(emailInput.getSearchText().get(),
                                    passwordInput.getSearchText().get(),
                                    finalNumeInput.getSearchText().get(),
                                    finalPrenumeInput.getSearchText().get(),
                                    finalAnInput.getSelectedItemIndex() + 1
                            )
                    );
                    if(registerStatus)
                        actionStatusLabel.setLabel("Inregistrarea a fost facuta cu success");
                    else
                        actionStatusLabel.setLabel("Datele introduse sunt invalide");
                }
            }
        });
        addComponent(loginButton);



        Label switcherLabel = new Label(mode.equals("Login") ? "Nu ai cont?" : "Ai deja cont?", true, 16);
        switcherLabel.setRatioedPosition(0.5f, 0.6f);
        addComponent(switcherLabel);

        Button switcherButton = new Button(mode.equals("Login") ? "Inregistreaza-te" : "Logheaza-te");
        switcherButton.setSize(300, 40);
        switcherButton.setRatioedPosition(0.5f,  0.65f);
        switcherButton.setListener(new ButtonListener() {
            @Override
            public void onClick() {
                updatePage();
            }
        });
        addComponent(switcherButton);
    }

    @Override
    protected void configurePrefabComponents(){
    }
}
