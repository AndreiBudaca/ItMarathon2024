package org.tuiasi.engine.networking;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data @AllArgsConstructor
public class OptionalDTO {
    private String name;
    private String description;
    private Integer preferenceIndex;
}
