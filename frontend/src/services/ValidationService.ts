import {Injectable} from '@angular/core';
import {LocalService} from './local.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Observable} from 'rxjs';
import {PaginatedList} from '../models/paginatedList';
import {CommandModel} from '../models/command.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {PlatformType} from '../enums/PlatformType';
import {ResultModel} from '../models/result.mode';

@Injectable({providedIn: "root"})
export class ValidationService {
  constructor(private client: HttpClient, private localService: LocalService, private jwtHelperService: JwtHelperService) {

  }

  private path: string = "api/openAi";

  getCommand(request: string, os: PlatformType): Observable<ResultModel> {
    let params = new HttpParams();

    params = params.append('os', os);

    const body = { request: request };

    return this.client.post<ResultModel>(`${this.path}/command/`, body, {params});
  }
}
