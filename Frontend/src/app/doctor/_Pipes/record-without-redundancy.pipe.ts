import { Pipe, PipeTransform } from '@angular/core';
import { Record } from 'src/app/_Models/record';
import { RecordPerDate } from 'src/app/_Models/recordPerDate';

@Pipe({
  name: 'recordWithoutRedundancy'
})
export class RecordWithoutRedundancyPipe implements PipeTransform {

  transform(records:Record[]): RecordPerDate[] {
    let temp:RecordPerDate=new RecordPerDate();
    let recordsPerDate:RecordPerDate[]=[];
    let indicator:boolean=true;
    for(let i=0;i<records.length;i++)
    {
      temp=new RecordPerDate();
      for(let j=0;j<records.length;j++)
      {
        if(records[i].date==records[j].date && records[i].pid==records[j].pid && records[i].did==records[j].did)
        {
          temp["did"]=records[i].did;
          temp["pid"]=records[i].pid;
          temp["date"]=records[i].date;
          temp["summary"]=records[i].summary;
          temp["prescription"]=records[i].prescription;
          temp["attached_files"].push(records[j].attached_files||null);
          temp["file_description"].push(records[j].file_description||"");
          temp["testType"].push(records[j].testType||"");
        }
      }

      recordsPerDate.forEach((record)=>{if(record.date==temp.date){indicator=false;}
                                        else{indicator=true;}})
      
      if(temp!=null && indicator)
      {
        recordsPerDate.push(temp);
        indicator=true;
      }
      
    }
    return recordsPerDate;
  }

}
