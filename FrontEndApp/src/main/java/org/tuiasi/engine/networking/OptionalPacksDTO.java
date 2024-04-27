package org.tuiasi.engine.networking;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;

@Data
public class OptionalPacksDTO {
    private List<OptionalPackDTO> optionalPacks;

    public OptionalPacksDTO(){
        optionalPacks = new ArrayList<>();
    }
}
