package org.tuiasi.engine.networking;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class RegisterDTO {
    public String email;
    public String password;
    public String lastName;
    public String firstName;
    public Integer yearOfStudy;
}
