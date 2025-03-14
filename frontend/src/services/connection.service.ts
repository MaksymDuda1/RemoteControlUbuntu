import { Observable } from "rxjs";
import { ConnectionModel } from "../models/connection.mode";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({providedIn: "root"})
export class ConnectionService{
    constructor(private client: HttpClient){

    }

    private path: string = "api/connections";

    getAllByUserId(id: number) : Observable<ConnectionModel[]>{
        return this.client.get<ConnectionModel[]>(this.path + '/user' + `/${id}`);
    }

    getById(id: number) : Observable<ConnectionModel>{
        return this.client.get<ConnectionModel>(this.path + `/${id}`);
    }

    getCommandCountByUserId(id: number): Observable<number>{
        return this.client.get<number>(this.path + `/${id}` + '/count')
    }

    create(connection: ConnectionModel) : Observable<any>{
        return this.client.post(this.path, connection);
    }

    update(id: number, connection: ConnectionModel): Observable<any> {
        return this.client.put(`${this.path}/${id}`, connection);
    }
    
    delete(id: number): Observable<any>{
        return this.client.delete(this.path + `/${id}`);
    }
}