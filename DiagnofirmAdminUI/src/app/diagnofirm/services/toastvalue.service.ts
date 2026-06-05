import { Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastvalueService{
  constructor(){}
  private toast = new BehaviorSubject<string>('false');
  casttoast = this.toast.asObservable();

  private value = new BehaviorSubject<string>('1');
  datavalue = this.value.asObservable();

  public arrvalue = new BehaviorSubject<any>(null);
  
  showtoast(newmeg:any){
    this.toast.next(newmeg); 
  }

  passvalue(data:any){
    this.value.next(data); 
  }

}