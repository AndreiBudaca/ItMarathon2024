package org.tuiasi.engine.networking;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;

@Data
public class OptionalPackDTO {
    private List<OptionalDTO> optionals;

    public OptionalPackDTO() {
        optionals = new ArrayList<>();
    }
}
