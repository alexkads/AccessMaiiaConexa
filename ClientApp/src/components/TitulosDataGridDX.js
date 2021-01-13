import React, { Component } from 'react';
import DataGrid, { 
    Column,
    Export, 
    ColumnChooser, 
    ColumnFixing, 
    Selection, 
    FilterRow, 
    GroupPanel,
    HeaderFilter,
    FilterPanel,
    FilterBuilderPopup,
    Summary,
    TotalItem,
    Pager, 
    Paging
} from 'devextreme-react/data-grid';
import { exportDataGrid } from 'devextreme/excel_exporter';
import CustomStore from 'devextreme/data/custom_store';
//import authService from './api-authorization/AuthorizeService';
import ExcelJS from 'exceljs';
import saveAs from 'file-saver';
import axios from "axios";
import './TitulosDataGridDX.css';

const filterBuilderPopupPosition = {
    of: window,
    at: 'top',
    my: 'top',
    offset: { y: 10 }
};

export class TitulosDataGridDX extends Component {
       
    constructor(props) {
        super(props);
        this.state = { titulos: [] };
        this.onExporting = this.onExporting.bind(this);
    }

    componentDidMount() {
        this.populateTitulosDataGrid();
    }



    onExporting(e) {
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet('Main sheet');
    
        exportDataGrid({
            component: e.component,
            worksheet: worksheet,
            autoFilterEnabled: true
            }).then(() => {
            workbook.xlsx.writeBuffer().then((buffer) => {
                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'DataGrid.xlsx');
            });
        });
        e.cancel = true;
    }

    render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel" >Titulos Maiia</h1>
                <DataGrid
                    id="grid" 
                    dataSource={this.state.titulos}
                    hoverStateEnabled={true}
                    showBorders={true}
                    allowColumnReordering={true}
                    allowColumnResizing={true}
                    columnAutoWidth={true}
                    rowAlternationEnabled={true}
                    onExporting={this.onExporting}>
                    <Selection mode="single" />
                    <FilterRow visible={true} />
                    <FilterPanel visible={true} />
                    <FilterBuilderPopup position={filterBuilderPopupPosition} />
                    <HeaderFilter visible={true} />
                    <Selection mode="multiple" />
                    <GroupPanel visible={true} />
                    <ColumnChooser enabled={true} />
                    <ColumnFixing enabled={true} />
                    <Paging defaultPageSize={10} />
                    <Pager
                        showPageSizeSelector={true}
                        allowedPageSizes={[5, 10, 25, 50, 100]}
                        showInfo={true} />
                    <Column dataField="id" />
                    <Column dataField="cnpj" width={150} fixed={true} caption="CNPJ/CPF" />
                    <Column dataField="unidade" width={150} caption="Unidade" visible={false}/>
                    <Column dataField="nomeunidade" width={150} caption="Nome da Unidade" />
                    <Column dataField="razao" width={500} caption="Razão Social"/>
                    <Column dataField="status" width={100} caption="Status"/>
                    <Column dataField="tipo" width={150} caption="Tipo de boleto"/>
                    <Column dataField="tipoentidade" width={150} caption="Tipo de Entidade"/>
                    <Column dataField="vencimento" width={150} dataType="date" format="dd/MM/yyyy"/>
                    <Column dataField="valor" format="#,##0.00" />
                    <Column dataField="pago" format="#,##0.00" />
                    <Column dataField="produto" width={150} caption="Produto" />
                    <Column dataField="parcela" width={150} caption="Parcela" />
                    <Column dataField="telefones_fixo" width={150} caption="Telefones Fixo" />
                    <Column dataField="telefones_celular" width={150} caption="Telefones Celular" />
                    <Column dataField="emails" width={150} caption="Emails" />
                    <Column dataField="ruadecorrepondencia" width={150} caption="Rua de Correspondência" />
                    <Column dataField="numerodecorrepondencia" width={150} caption="Número de Correspondência" />
                    <Column dataField="complementodecorrepondencia" width={150} caption="Complemento de Correspondência" />
                    <Column dataField="bairrodecorrepondencia" width={150} caption="Bairro de Correspondência" />
                    <Column dataField="cepdecorrepondencia" width={150} caption="CEP de Correspondência" />
                    <Column dataField="cidadedecorrepondencia" width={150} caption="Cidade de Correspondência" />
                    <Column dataField="ie" width={150} caption="IE" />
                    <Column dataField="rg" width={150} caption="RG" />

                    <Summary>
                        <TotalItem
                            column="valor"
                            summaryType="sum"
                            valueFormat="#,##0.00" />
                    </Summary>
                    <Export enabled={true} allowExportSelectedData={true} />
                </DataGrid>
            </React.Fragment>
        );
    }

    async populateTitulosDataGrid() {
        const token = JSON.parse(sessionStorage.getItem('token'));
        const headers = !token ? {} : { 'headers': { 'Authorization': `Bearer ${token}` } };
        
        const store = new CustomStore({
            key: 'id',
            load: async function(loadOptions) {              
                return axios.get(`titulos`, headers)
                    .then(response => response.data)
                    .catch(() => { throw new Error('Data Loading Error'); });
            }
        });
       
        this.setState({ titulos: store });
    }   
}