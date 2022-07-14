import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Appdate } from 'src/app/_Models/date';
import { Visit } from 'src/app/_Models/visit';
import { Works_in } from 'src/app/_Models/works_in';
import { PatientService } from '../Patient.service';
import { IsValidDatePipe } from '../_Pipes/is-valid-date.pipe';

@Component({
  selector: 'app-editappointment',
  templateUrl: './editappointment.component.html',
  styleUrls: ['./editappointment.component.css']
})
export class EditappointmentComponent implements OnInit,OnDestroy {

  visit:Visit=new Visit();
  doctorId:number=0;
  patientId:number=0;
  appointmentTime:Date=new Date()
  appointments:Works_in[]=[];
  sub:Subscription|null=null;
  sub2:Subscription|null=null;
  bookingDate:Date|null=null;

  constructor(private isValid:IsValidDatePipe,private datePipe:DatePipe,private patientser:PatientService,private activateRoute:ActivatedRoute,private router:Router) { }

  ngOnInit(): void {
    this.sub=this.activateRoute.params.subscribe(
      a=>{
        this.patientser.GetAppointments(a['did']).subscribe(
          data=>{
            this.appointments=data;
            this.doctorId=a['did'];
          }
        )
        this.patientId = a["id"]
        this.doctorId = a["did"]
        this.appointmentTime = a["appointment_time"]
        this.visit.pid = this.patientId
      }
    ) 
    }

    
  date:string='';
  fromdate:string='';
  todate:string='';
  appointmentDate:Date|null=null;
  maxpatientNo:number=Number();
  retreived:Appdate|null=null;
  errorMessage:string='';
    
  Update(){
    this.date=this.datePipe.transform(this.visit.appointment_time,'fullDate')!; //required date
    console.log(this.date)
    this.fromdate=this.datePipe.transform(new Date(),'fullDate')!; //min date
    this.todate = this.datePipe.transform(new Date(new Date(this.fromdate).setMonth((new Date(this.fromdate)).getMonth()+2)),'fullDate')!; //max date

    //check validation of date
    this.retreived=this.isValid.transform(this.date,this.fromdate,this.todate,this.appointments);
    console.log(this.retreived)

    if(this.retreived) // if valid
    {
     this.appointmentDate=this.retreived.AppointmentDate;
     this.maxpatientNo=this.retreived.maxpatientNo;

      //check if this appointment is filled or not
      this.patientser.countPatients(this.doctorId,this.datePipe.transform(this.appointmentDate,'fullDate')!).subscribe(
        No=>{
          if( No==0 || No<=this.maxpatientNo) //not filled
          {
            this.visit.did = this.doctorId;
            this.patientser.getPatientId();
            this.visit.pid = this.patientser.PatientID;
            this.visit.appointment_time = this.datePipe.transform(this.appointmentDate,'short')!;
            console.log(this.visit.appointment_time)
            this.patientser.editAppointment(this.visit.pid,this.visit.did,this.appointmentTime,this.visit).subscribe(
              data=>{
                  this.router.navigateByUrl("patient/appointments");
              },
              err=>{ //book before
                this.errorMessage= "You already booked this appointment";
              }
            )
          }
          else //filled
          {
            this.errorMessage= "This appointment filled";
          }
        }
      )
    }
    else //not available
    {
      this.errorMessage= "This appointment date is not available , Please book in available date";
    }
    }
    ngOnDestroy(): void {
      this.sub?.unsubscribe();
    }
  }