import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import '../../App.css';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Loader from 'react-loader-spinner'
import moment from 'moment';
import './agendamento.css';


class agendamento extends Component {

    constructor(props) {
        super(props);
        this.state = {
            vaga: '',
            fornecedor: '',
            opcoesFornecedor: [],
            listaAgendamentos: [],
            loader: true,
            anoAtual: (new Date()).getFullYear(),
            data: new Date()
        };

        this.handleChangeVaga = this.handleChangeVaga.bind(this);
        this.handleChangeFornecedor = this.handleChangeFornecedor.bind(this);
        this.agendar = this.agendar.bind(this);
    }

    componentDidMount() {
        this.obterFornecedores();
    }

    render() {
        return (
            <div>
                {this.state.loader === true ?
                    <div className="container loader">
                        <Loader
                            type="Puff"
                            color="black"
                            height="400"
                            width="100"
                        />
                    </div>
                    :
                    <div>
                        <div>
                            <header>
                                <h2>Sistema de Agendamento</h2>
                            </header>
                            <form onSubmit={this.agendar}>
                                <h5>Formulário de cadastro:</h5>
                                <div className="row formulario">
                                    <div>
                                        <label>
                                            Vaga:
                                        </label>
                                        <select value={this.state.vaga} onChange={this.handleChangeVaga}>
                                            <option value="">Selecione...</option>
                                            <option value="0">Vaga A</option>
                                            <option value="1">Vaga B</option>
                                            <option value="2">Vaga C</option>
                                        </select>
                                    </div>
                                    <div>
                                        <label>
                                            Fornecedor:
                                        </label>
                                        <select value={this.state.fornecedor} onChange={this.handleChangeFornecedor}>
                                            <option value="">Selecione...</option>
                                            {this.state.opcoesFornecedor}
                                        </select>
                                    </div>
                                    <div>
                                        <label>Data:</label>
                                        <DatePicker
                                            selected={this.state.data}
                                            onChange={date => this.setState({ data: date })}
                                            locale="pt-BR"
                                            showTimeSelect
                                            timeFormat="p"
                                            timeIntervals={15}
                                            dateFormat="dd/MM/yyyy - HH:mm"
                                            minDate={new Date()}
                                        />
                                    </div>

                                    <input type="submit" value="Salvar" />
                                </div>


                            </form>
                        </div>
                        <div className="cadastros">
                            <h5>Cadastros realizados:</h5>
                            <table className="table table-bordered" width="100%" cellSpacing="0">
                                <thead>
                                    <tr>
                                        <th>Data Agendada</th>
                                        <th>Fornecedor</th>
                                        <th>Vaga</th>
                                        <th>Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.preencheTabelaEventos()}
                                </tbody>
                            </table>
                        </div>
                        <footer>
                            <h6> © {this.state.anoAtual} Alazom - Todos os direitos reservados.</h6>
                        </footer>
                    </div>
                }
            </div>
        )
    }

    obterFornecedores = () => {
        axios.get('http://localhost:5000/api/fornecedor')
            .then(resposta => {
                this.criarOpcoesFornecedor(resposta.data);
            })
            .catch(error => {
                this.setState({ loader: false })
                toast.error("Não foi possível carregar a lista de fornecedores.");
            });
    }

    criarOpcoesFornecedor = (fornecedores) => {
        let opcoesFornecedor = this.state.opcoesFornecedor;

        opcoesFornecedor = fornecedores.map((e, key) => {
            return <option key={key} value={e.id}>{e.nome}</option>;
        })

        this.setState({ opcoesFornecedor: opcoesFornecedor, loader: false })

        this.obterAgendamentos();
    }

    obterAgendamentos = () => {
        axios.get('http://localhost:5000/api/agendamento')
            .then(resposta => {
                this.setState({ listaAgendamentos: resposta.data });
                this.preencheTabelaEventos();
            })
            .catch(error => {
                this.setState({ loader: false })
                toast.error("Não foi possível obter os registros de agendamento.");
            });
    }

    preencheTabelaEventos = () => {
        var listaRetorno = []
        let vaga = '';

        if (this.state.listaAgendamentos.length === 0) {
            listaRetorno.push(
                <tr>
                    <td colSpan="4">
                        Nenhum agendamento cadastrado.</td>
                </tr>
            );
        }
        else {
            this.state.listaAgendamentos.forEach((agendamento, index) => {
                switch (agendamento.vaga) {
                    case 0:
                        vaga = 'A';
                        break;

                    case 1:
                        vaga = 'B';
                        break;

                    case 0:
                        vaga = 'C';
                        break;

                    default:
                        break;
                }
                listaRetorno.push(
                    <tr>
                        <td className="text-center">
                            {moment(agendamento.data).format('DD/MM/yyyy - HH:mm')}
                        </td>
                        <td className="text-center">
                            {agendamento.fornecedor}
                        </td>
                        <td className="text-center">
                            {vaga}
                        </td>
                        <td className="text-center">
                            <div className="opcoesEvento">
                                <i className="fa fa-trash-o" aria-hidden="true" title="Excluir Agendamento" onClick={() => this.excluirAgendamento(agendamento.idAgendamento)}></i>
                            </div>
                        </td>
                    </tr>
                );
            })
        }

        return listaRetorno;
    }

    handleChangeVaga(event) {
        this.setState({ vaga: event.target.value });
    }

    handleChangeFornecedor(event) {
        this.setState({ fornecedor: event.target.value });
    }

    agendar(event) {
        this.setState({ loader: true })
        event.preventDefault();

        if (this.validarCampos()) {
            axios.post(`http://localhost:5000/api/agendamento?dataAgendamento=${this.state.data.toISOString()}&idFornecedor=${this.state.fornecedor}&vaga=${this.state.vaga}`)
                .then(resposta => {
                    this.setState({ loader: false })
                    toast.success("Agendamento realizado com sucesso!");
                    this.obterAgendamentos();
                })
                .catch(error => {
                    this.setState({ loader: false })
                    toast.error(error.response.data);
                });
        } else {
            this.setState({ loader: false })
        }
    }

    validarCampos = () => {
        if (this.state.vaga === "") {
            toast.error("A vaga deve ser informada.");
            return false;
        }

        if (this.state.fornecedor === "") {
            toast.error("O fornecedor deve ser informado.");
            return false;
        }

        if (this.state.data === null) {
            toast.error("A data deve ser informada.");
            return false;
        }

        return true;
    }

    excluirAgendamento = (id) => {
        axios.delete(`http://localhost:5000/api/agendamento?idAgendamento=${id}`)
            .then(resposta => {
                this.setState({ loader: false })
                toast.success("Agendamento excluído com sucesso!");
                this.obterAgendamentos();
            })
            .catch(error => {
                console.log(error.response)
                this.setState({ loader: false })
                toast.error(error.response.data);
            });
    }
}

export default withRouter(agendamento);
