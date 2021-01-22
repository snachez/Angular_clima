import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Student } from '../Models/StudentModel';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  AddStudent(student: Student) {
    const data = new Student();
    data.FirstName = student.FirstName;
    data.LastName = student.LastName;
    data.City = student.City;
    data.State = student.State;
    this.http.post('https://localhost:5001/api/Student/CrearStudent', data)
      .subscribe((resp) => {
        console.log(resp);
      });
  }

  getAllStudent() {
   return this.http.get('https://localhost:5001/api/Student');
  }

  getuserName(): string {
    return 'Pedro';
  }

  getuserLastName() {
    console.log('Julio');
  }

}
