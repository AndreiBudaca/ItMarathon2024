package org.tuiasi.engine.ui.components.basicComponents.searchbar;

import imgui.ImGui;
import imgui.flag.ImGuiInputTextFlags;
import imgui.type.ImString;
import lombok.*;

@Getter
@Setter
@NoArgsConstructor
public class SearchbarWithHint extends ISearchbar {

    private String label;
    private String hint = "Search";
    private ImString searchText = new ImString();
    private boolean enterPressed = false;

    private boolean isPassword = false;

    private SearchListener searchListener;

    public SearchbarWithHint(String hint) {
        this.hint = hint;
    }

    public SearchbarWithHint(String label, String hint) {
        this.label = label;
        this.hint = hint;
    }

    public SearchbarWithHint(String label, String hint, boolean isPassword) {
        this.label = label;
        this.hint = hint;
        this.isPassword = isPassword;
    }

    @Override
    public void render() {

        ImGui.setNextItemWidth(getWidth());
        ImGui.setCursorPosX((ImGui.getWindowSizeX() - getWidth()) * getRatioX());
        ImGui.setCursorPosY((ImGui.getWindowSizeY() - getHeight()) * getRatioY());

        int flags = isPassword ? ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.Password : ImGuiInputTextFlags.EnterReturnsTrue;

        enterPressed = ImGui.inputTextWithHint("##Searchbar_" + label, hint, searchText, flags);
        ImGui.sameLine();
        if (enterPressed && searchListener != null) {
            searchListener.onSearch(searchText.get());
        }
        ImGui.newLine();
    }

}