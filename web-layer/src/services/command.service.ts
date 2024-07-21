import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CommandModel } from "../models/command.model";
import { ConnectionCommandModel } from "../models/connectionCommand.model";

@Injectable({providedIn: "root"})
export class CommandService{
    constructor(private client: HttpClient){

    }

    private path: string = "api/execute";

    execute(connectionCommandModel: ConnectionCommandModel){
        return this.client.post(this.path, connectionCommandModel);
    }
}