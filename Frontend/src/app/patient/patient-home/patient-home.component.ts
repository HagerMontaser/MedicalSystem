import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { Patient } from 'src/app/_Models/patient';
import { PatientService } from '../Patient.service';
import { LoadingComponent } from 'src/app/core/loading/loading.component';
@Component({
  selector: 'app-patient-home',
  templateUrl: './patient-home.component.html',
  styleUrls: ['./patient-home.component.css'],
})
export class PatientHomeComponent implements OnInit, OnDestroy {
  par: string = '';
  sub: Subscription | null = null;
  patient = new Patient();
  private patient$ = new BehaviorSubject<any>({});
  selectedPatient$ = this.patient$.asObservable();
  constructor(public router: Router, public patientService: PatientService) {}

  ngOnInit(): void {
    this.sub = this.patientService.getPatient().subscribe({
      next: (data) => {
        this.patient = data;
        this.patient$.next(this.patient);

        console.log('patient: ', this.patient);
      },
      error: (err) => {
        console.log('error from patient home component: ', err);
      },
    });
  }

  send() {
    this.patient$.next(this.patient);
  }

  getSelectedPatient(): Observable<Patient> {
    return new Observable((obs) => {
      obs.next(this.patient);
    });
  }

  ngOnDestroy(): void {
      this.sub?.unsubscribe();
  }
}
