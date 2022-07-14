import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Doctor } from '../_Models/doctor';
import { Other } from '../_Models/other';
import { Patient } from '../_Models/patient';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(public http: HttpClient, public router: Router) { }
  baseUrl = "https://localhost:7089/api/";


  //add doctor 
  addDoctor(user: Doctor) {
    return this.http.post<any>(this.baseUrl + "doctor", user);
  }

  //add other 
  addOther(user: Other) {
    return this.http.post<any>(this.baseUrl + "other", user);
  }

  //add other 
  getPatients() {
    return this.http.get<Patient[]>(this.baseUrl + "Patient/adminFilesPatients");
  }


  uploadFile(record: any, fileToUpload: any) {
    return this.http.post<any>(this.baseUrl + 'Record/AddFile/' + record.pid + '/' + record.did + '/' + record.date + '/' + record.file_description + '/' + record.fno + '/' + record.oid, fileToUpload);
  }

  DeleteFile(record: any) {
    return this.http.delete<any>(this.baseUrl + 'Record/DeleteFile/' + record.pid + '/' + record.did + '/' + record.date + '/' + record.file_description + '/' + record.fno + '/' + record.oid);
  }
}
