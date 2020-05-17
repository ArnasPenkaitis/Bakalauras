package com.example.bakalauras.POJO;

public class SubjectListItemPOJO {
    private final String TAG = "SUBJECT_LIST_ITEM_POJO";

    private String Id;
    private String Name;

    public SubjectListItemPOJO(String id, String name) {
        this.Id = id;
        this.Name = name;
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
}
