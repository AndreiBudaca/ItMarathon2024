package org.tuiasi.engine.networking;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data @AllArgsConstructor
public class LoginDTO {
    public String email;
    public String password;
}
