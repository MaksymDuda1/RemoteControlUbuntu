import { Observable } from "rxjs";
import { ConnectionModel } from "../models/connection.mode";
import {HttpClient, HttpParams} from "@angular/common/http";
import { Injectable } from "@angular/core";
import {PaginatedList} from '../models/paginatedList';

@Injectable({providedIn: "root"})
export class ConnectionService{
    constructor(private client: HttpClient){

    }

    private path: string = "api/connection";

    getAllByUserId(id: number, filters?: any,) : Observable<PaginatedList<ConnectionModel>>{
      let params = new HttpParams();

      if (filters) {
        if (filters.name) {
          params = params.set('name', filters.name);
        }
        if (filters.username) {
          params = params.set('username', filters.username);
        }
        if (filters.host) {
          params = params.set('host', filters.host);
        }
        if (filters.pagination?.pageIndex){
          params = params.set('pageIndex', filters.pagination.pageIndex);
        }
        if (filters.pagination?.pageSize){
          params = params.set('pageSize', filters.pagination.pageSize);
        }
      }

      return this.client.get<PaginatedList<ConnectionModel>>(this.path + '/user' + `/${id}`, { params });
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
