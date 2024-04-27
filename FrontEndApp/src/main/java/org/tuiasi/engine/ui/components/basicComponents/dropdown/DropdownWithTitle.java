package org.tuiasi.engine.ui.components.basicComponents.dropdown;

import imgui.ImGui;
import imgui.flag.ImGuiHoveredFlags;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter @Setter @NoArgsConstructor @AllArgsConstructor
public class DropdownWithTitle extends IDropdown{

    private String label;
    private String[] items;
    private int selectedItemIndex;
    private DropdownListener listener;

    private float xRatioToWindow = 0f;

    public DropdownWithTitle(String label, String[] items) {
        this.label = label;
        this.items = items;
        this.selectedItemIndex = 0; // Default to the first item
    }

    @Override
    public void render() {
        if(xRatioToWindow != 0f)
            setWidth(ImGui.getWindowSizeX() * xRatioToWindow);

        ImGui.setNextItemWidth(getWidth());

        ImGui.setCursorPosX((ImGui.getWindowSizeX() - getWidth()) * getRatioX());
        ImGui.setCursorPosY((ImGui.getWindowSizeY() - getHeight()) * getRatioY());


        if (ImGui.beginCombo(label + "##Dropdown", items[selectedItemIndex])) {

            for (int i = 0; i < items.length; i++) {

                // Render the dropdown
                if(ImGui.isItemHovered(ImGuiHoveredFlags.AllowWhenBlockedByActiveItem | ImGuiHoveredFlags.AllowWhenDisabled
                | ImGuiHoveredFlags.AllowWhenOverlapped)) {
                    ImGui.setTooltip(items[i-1]);
                }

                boolean isSelected = selectedItemIndex == i;
                if (ImGui.selectable(items[i] + "##OptionOfDropdown_" + label, isSelected)) {

                    selectedItemIndex = i;
                    if (listener != null) {
                        listener.onItemSelected(i);
                    }
                }
                if (isSelected) {
                    ImGui.setItemDefaultFocus();
                }
            }

            if(ImGui.isItemHovered(ImGuiHoveredFlags.AllowWhenBlockedByActiveItem | ImGuiHoveredFlags.AllowWhenDisabled
                    | ImGuiHoveredFlags.AllowWhenOverlapped)) {
                ImGui.setTooltip(items[items.length - 1]);
            }

            ImGui.endCombo();
        }
    }

}
