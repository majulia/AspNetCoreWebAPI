import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Util } from '../../../util/util'
import { Disciplina } from 'src/app/models/Disciplina';
import { Professor } from 'src/app/models/Professor';

@Component({
  selector: 'app-professores-alunos',
  templateUrl: './professores-alunos.component.html',
  styleUrls: ['./professores-alunos.component.css']
})
export class ProfessoresAlunosComponent implements OnInit {

  @Input() public professores: Professor[];
  @Output() closeModal = new EventEmitter();

  constructor(private router: Router) { }

  ngOnInit() {
  }

  disciplinaConcat(disciplinas: Disciplina[]) {
    return Util.nomeConcat(disciplinas);
  }

  professorSelect(prof: Professor) {
    this.closeModal.emit(null);
    this.router.navigate(['/professor', prof.id]);
  }

}
