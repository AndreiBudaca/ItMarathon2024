package org.tuiasi.engine.networking;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data @NoArgsConstructor
public class OptionalPackDTO {
    private List<OptionalDTO> optionals;
}
