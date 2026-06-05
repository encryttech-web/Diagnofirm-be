import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AvatarService {

  constructor() {}

  getFirstName(): string {
    return 'John';  
  }

  getLastName(): string {
    return 'Doe';  
  }
}
