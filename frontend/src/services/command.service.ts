import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CommandModel } from "../models/command.model";
import { Observable } from "rxjs";

@Injectable({providedIn: "root"})
export class CommandService{
    constructor(private client: HttpClient){

    }

    private path: string = "api/commads";

    getAllByUserId(id: number) : Observable<CommandModel[]>{
        return this.client.get<CommandModel[]>(this.path + '/user' + `/${id}`);
    }

    getById(id: number) : Observable<CommandModel>{
        return this.client.get<CommandModel>(this.path + `/${id}`);
    }

    getCommandCountByUserId(id: number): Observable<number>{
        return this.client.get<number>(this.path + `/${id}` + '/count')
    }

    create(command: CommandModel) : Observable<any>{
        return this.client.post(this.path, command);
    }

    update(id: number, command: CommandModel): Observable<any> {
        return this.client.put(`${this.path}/${id}`, command)
        }
    
    delete(id: number): Observable<any>{
        return this.client.delete(this.path + `/${id}`);
    }
}