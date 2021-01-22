import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { FormControl, FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Student } from '../../Models/StudentModel';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  miFormulario: FormGroup;
  constructor(private userService: UserService, private fb: FormBuilder,
              private toastr: ToastrService) {
  }

  listData: MatTableDataSource<any>;
  displayedColumns: string[] = ['studentId', 'firstName', 'lastName', 'state', 'city', 'actions'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchKey: string;

  showSuccess(){
    this.toastr.success('Se realizo correctamente!', 'Información!');
  }

  ngOnInit() {
    this.miFormulario = this.fb.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      City: [''],
      State: [''],
    });
  }
  onSubmit(formValue: any) {
    const student = new Student();
    student.FirstName = formValue.FirstName;
    student.LastName = formValue.LastName;
    student.City = formValue.City;
    student.State = formValue.State;
    this.userService.AddStudent(student);
    this.showSuccess();
    /*setTimeout(() => {
    this.userService.getAllStudent().subscribe(
      list => {
      let array = list.map(item => {
        return {
          $key: item.key,
          ...item.payload.val()
        };
      });
      this.listData = new MatTableDataSource(array);
      this.listData.sort = this.sort;
      this.listData.paginator = this.paginator;
      this.listData.filterPredicate = (data, filter) => {
        return this.displayedColumns.some(ele => {
          return ele != 'actions' && data[ele].toLowerCase().indexOf(filter) != -1;
        });
      };
    });
  }, 3000);*/
  }

  onSearchClear() {
    this.searchKey = '';
    this.applyFilter();
  }

  applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

}
