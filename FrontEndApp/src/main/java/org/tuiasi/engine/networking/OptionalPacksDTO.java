package org.tuiasi.engine.networking;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data @NoArgsConstructor
public class OptionalPacksDTO {
    private List<OptionalPackDTO> optionalPacks;
}
