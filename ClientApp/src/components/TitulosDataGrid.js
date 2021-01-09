import React, { Component } from 'react';
//import authService from './api-authorization/AuthorizeService'
import ExcelJS from 'exceljs'
import { saveAs } from 'file-saver'

export class TitulosDataGrid extends Component {

    constructor(props) {
        super(props);
        this.state = { titulos: [], loading: true };
    }

    componentDidMount() {
        this.populateTitulosDataGrid();
    }

    onExportTitulos(titulos){
        const workbook = new ExcelJS.Workbook();
        workbook.creator = "test";
        workbook.lastModifiedBy = "test";
        workbook.created = new Date();
        workbook.modified = new Date();

        let sheet = workbook.addWorksheet("Titulos");

        sheet.getRow(1).values = [ "Id", "Vencimento", "Entidade" ];

        sheet.columns = [
            { key: "id", width: 30 },
            { key: "vencimento", width: 30 },
            { key: "entidadeId", width: 30 }
        ];

        sheet.addRows(titulos)

        const row = sheet.getRow(1);
        
        row.eachCell((cell, rowNumber) => {
          sheet.getColumn(rowNumber).alignment = {
            vertical: "middle",
            horizontal: "center"
          };
          sheet.getColumn(rowNumber).font = { size: 14, family: 2 };
        });

        workbook.xlsx.writeBuffer().then(function(buffer) {
            const blob = new Blob([buffer], { type: "applicationi/xlsx" });
            saveAs(blob, 'abc.xlsx')
        });
    }

    static renderTitulosTable(titulos) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Vencimento</th>
                        <th>Código da Entidade</th>
                    </tr>
                </thead>
                <tbody>
                    {titulos.map(titulo =>
                        <tr key={titulo.id}>
                            <td>{titulo.id}</td>
                            <td>{titulo.vencimento}</td>
                            <td>{titulo.entidadeId}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Carregando...</em></p>
            : TitulosDataGrid.renderTitulosTable(this.state.titulos);

        return (
            <div>
                <h1 id="tabelLabel" >Titulos Maiia</h1>
                <button className="btn btn-primary" onClick={()=> this.onExportTitulos(this.state.titulos)}>Export XLS</button>
                {contents}
            </div>
        );
    }

/*     async populateTitulosDataGridToken() {
        const token = await authService.getAccessToken();
        const response = await fetch('titulos', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ titulos: data, loading: false });
    } */

    async populateTitulosDataGrid() {
        const response = await fetch('titulos');
        const data = await response.json();
        this.setState({ titulos: data, loading: false });
    }
}