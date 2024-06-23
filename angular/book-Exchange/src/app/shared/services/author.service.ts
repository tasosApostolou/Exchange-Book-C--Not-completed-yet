import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { AuthorInsertDTO } from '../interfaces/author-insert-dto';
const API_URL = `${environment.apiURL}/author`

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  // constructor() { }
  http: HttpClient = inject(HttpClient)
registerAuthor(authorInsertDTO:AuthorInsertDTO){
  return this.http.post<{data:JSON}>(`${API_URL}/authors`,authorInsertDTO)
  


}
}
