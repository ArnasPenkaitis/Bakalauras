export class Student {
    public id: number;
    public name: string;
    public surname: string;
    public username: string;
    public password: string;
    public email: string;

    constructor(id: number, name: string, surname: string, username: string, password: string, email: string) {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.username = username;
        this.password = password;
        this.email = email;
    }
}
