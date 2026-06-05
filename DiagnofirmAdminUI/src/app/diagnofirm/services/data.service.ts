import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GlobalConstants } from "./global.constant";

@Injectable({
    providedIn: 'root'
})
export class DataService {
    private apiUrl = GlobalConstants.Authurl;

    constructor(private http: HttpClient) { }

    getData(url:string): Observable<any> {
        return this.http.get(url);
    }

    addData(url: string, newData: any): Observable<any> {
        return this.http.post(url, newData);
    }

    updateData(id: number, updatedData: any): Observable<any> {
        return this.http.put(`${this.apiUrl}/${id}`, updatedData);
    }

}