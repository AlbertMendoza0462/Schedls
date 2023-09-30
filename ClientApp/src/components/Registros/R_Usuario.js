import React, { useContext, useEffect, useRef, useState } from 'react';
import axios from 'axios'
import { ApiDelete, ApiPost } from '../../Api';
import { AlertContext } from '../../Contexts';
import { SuccessAlert, ErrorAlert, AlertMessage } from '../Alertas';
import { DeleteConfirm, SaveConfirm } from '../Confirmaciones';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

const Usuario = (props) => {
    const alertContext = useContext(AlertContext)
    const [loading, setLoading] = useState([])
    const [usuarioId, setUsuarioId] = useState(0)
    const [nombre, setNombre] = useState("")
    const [apellido, setApellido] = useState("")
    const [correo, setCorreo] = useState("")
    const [clave, setClave] = useState("")

    const handleSubmit = (e) => {
        e.preventDefault()

        const usuario = {
            UsuarioId: usuarioId,
            Nombre: nombre,
            Apellido: apellido,
            Correo: correo,
            Clave: clave
        }
        console.log(usuario)


        SaveConfirm(() => {
            return new Promise((resolve, reject) => {
                ApiPost("/api/Usuario", usuario)
                    .then(d => {
                        alertContext.setAlertas((al) => [...al, {
                            Alerta: SuccessAlert,
                            mensaje: AlertMessage.guardado
                        }])
                        console.log(d.response)
                        props.setIsOpen(false)
                        props.solicitarData()
                        setLoading(false)
                        resolve()
                    })
                    .catch(d => {
                        alertContext.setAlertas((al) => [...al, {
                            Alerta: ErrorAlert,
                            mensaje: AlertMessage.noGuardado
                        }])
                        console.log(d)
                        setLoading(false)
                        reject()
                    })
            })
        })
    }

    const handleDelete = () => {
        DeleteConfirm(() => {
            return new Promise((resolve, reject) => {
                ApiDelete("/api/Usuario", usuarioId)
                    .then(d => {
                        alertContext.setAlertas((al) => [...al, {
                            Alerta: SuccessAlert,
                            mensaje: AlertMessage.guardado
                        }])
                        console.log(d.response)
                        props.setIsOpen(false)
                        props.solicitarData()
                        setLoading(false)
                        resolve()
                    })
                    .catch(d => {
                        alertContext.setAlertas((al) => [...al, {
                            Alerta: ErrorAlert,
                            mensaje: AlertMessage.noGuardado
                        }])
                        console.log(d)
                        setLoading(false)
                        reject()
                    })
            })
        })
    }

    useEffect(() => {
        if (props.entidad != null) {
            setUsuarioId(props.entidad.empleadoId)
            setNombre(props.entidad.nombre)
            setApellido(props.entidad.apellido)
            setCorreo(props.entidad.correo)
        } else {
            setUsuarioId(0)
            setNombre("")
            setApellido("")
            setCorreo("")
        }
    }, [props.entidad])

    return (
        <div>
            <Modal isOpen={props.isOpen} fullscreen={true} toggle={() => props.setIsOpen(false)}>
                <form onSubmit={handleSubmit}>
                    <ModalHeader toggle={() => props.setIsOpen(false)}>
                        <h1>Registro de Usuario</h1>
                    </ModalHeader>
                    <ModalBody>
                        <div className="row d-flex justify-content-center">
                            <div className="col-md-4">
                                <div className="text-danger"></div>
                                <div className="form-group">
                                    <label className="control-label">Nombre</label>
                                    <input className="form-control" id="Nombre" name="Nombre" value={nombre} onChange={(e) => setNombre(e.target.value)} />
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <label className="control-label">Apellido</label>
                                    <input className="form-control" id="Apellido" name="Apellido" value={apellido} onChange={(e) => setApellido(e.target.value)} />
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <label className="control-label">Correo</label>
                                    <input type="email" className="form-control" id="Correo" name="Correo" value={correo} onChange={(e) => setCorreo(e.target.value)} />
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <label className="control-label">Clave</label>
                                    <input className="form-control" id="Clave" name="Clave" type="password" value={clave} onChange={(e) => setClave(e.target.value)} />
                                    <span className="text-danger"></span>
                                </div>
                            </div>
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <div className="form-group">
                            <input type="submit" className="btn btn-primary" />
                        </div>
                        {(props.entidad != null) ?
                            <>
                                <Button color="danger" onClick={handleDelete}>Eliminar</Button>
                            </>
                            :
                            <></>
                        }
                        <Button color="secondary" onClick={() => props.setIsOpen(false)}>Cancel</Button>
                    </ModalFooter>
                </form>
            </Modal>
        </div >
    )
}

export default Usuario