import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Camel } from '../models/camel';

@Injectable({ providedIn: 'root' })
export class CamelService {
  private url = `${environment.apiBaseUrl}/api/camels`;

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Camel[]>(this.url);
  }
}
