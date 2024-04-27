package org.tuiasi.engine.ui.uiWindows;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data @NoArgsConstructor @AllArgsConstructor
public class Page {
    private String name;
    private List<IUIWindow> iuiWindows;
}
