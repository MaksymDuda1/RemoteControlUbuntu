import {HttpClient, HttpParams} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CommandModel } from "../models/command.model";
import { Observable } from "rxjs";
import {PaginatedList} from '../models/paginatedList';
import {ConnectionModel} from '../models/connection.mode';

@Injectable({providedIn: "root"})
export class CommandService{
    constructor(private client: HttpClient){

    }

    private path: string = "api/commads";

  getAllByUserId(id: number, filters?: any,) : Observable<PaginatedList<CommandModel>>{
    let params = new HttpParams();

    if (filters) {
      if (filters.name) {
        params = params.set('name', filters.name);
      }
      if (filters.command) {
        params = params.set('username', filters.username);
      }
      if (filters.type) {
        params = params.set('type', filters.type);
      }
      if (filters.dateFrom) {
        params = params.set('dateFrom', filters.dateFrom);
      }
      if (filters.dateTo) {
        params = params.set('dateTo', filters.dateTo);
      }
      if (filters.pagination?.pageIndex){
        params = params.set('pageIndex', filters.pagination.pageIndex);
      }
      if (filters.pagination?.pageSize){
        params = params.set('pageSize', filters.pagination.pageSize);
      }
    }

    return this.client.get<PaginatedList<CommandModel>>(this.path + '/user' + `/${id}`, { params });
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
