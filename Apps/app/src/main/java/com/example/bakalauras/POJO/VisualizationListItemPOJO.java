package com.example.bakalauras.POJO;

public class VisualizationListItemPOJO {
    private final String TAG = "VISUALIZATION_LIST_ITEM_POJO";

    private String Id;
    private String Name;
    private String Description;
    private String FileUrl;


    public VisualizationListItemPOJO(String id, String name, String description, String fileUrl) {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.FileUrl = fileUrl;
    }

    public String getId() {
        return Id;
    }
    public void setId(String id) {
        Id = id;
    }

    public String getName() {
        return Name;
    }
    public void setName(String name) {
        Name = name;
    }

    public String getDescription() {
        return Description;
    }
    public void setDescription(String description) {
        Description = description;
    }

    public String getFileUrl() {
        return FileUrl;
    }
    public void setFileUrl(String fileUrl) {
        FileUrl = fileUrl;
    }
}
