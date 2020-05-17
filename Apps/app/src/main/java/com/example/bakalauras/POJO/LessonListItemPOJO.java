package com.example.bakalauras.POJO;

public class LessonListItemPOJO {
    private final String TAG = "LESSON_LIST_ITEM_POJO";

    private String Id;
    private String Name;
    private String Abbreviation;

    public LessonListItemPOJO(String id, String name, String abbreviation) {
        this.Id = id;
        this.Name = name;
        this.Abbreviation = abbreviation;
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

    public String getAbbreviation() {
        return Abbreviation;
    }
    public void setAbbreviation(String abbreviation) {
        Abbreviation = abbreviation;
    }

}

