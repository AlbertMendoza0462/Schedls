import React, { useContext, useEffect, useState } from 'react'
import FullCalendar from '@fullcalendar/react'
import dayGridPlugin from '@fullcalendar/daygrid'
import timeGridPlugin from '@fullcalendar/timegrid'
import interactionPlugin from "@fullcalendar/interaction"
import multiMonthPlugin from '@fullcalendar/multimonth'
import esLocale from '@fullcalendar/core/locales/es'
import rrulePlugin from '@fullcalendar/rrule'
import momentPlugin, { toMomentDuration } from '@fullcalendar/moment'
import { AlertContext } from '../../Contexts'
import { ApiGet } from '../../Api'
import { AlertMessage, ErrorAlert, SuccessAlert } from '../Alertas'

const Calendario = () => {
    const alertContext = useContext(AlertContext)
    const [data, setData] = useState([])
    const [aprobadas, setAprobadas] = useState([])
    const [loading, setLoading] = useState([])

    const solicitarData = () => {
        ApiGet("/api/Turno/eventos")
            .then(d => {
                d.response.map((turno) => {
                    turno.title = turno.title
                    turno.duration = "01:00:00"
                })
                alertContext.setAlertas((al) => [...al, {
                    Alerta: SuccessAlert,
                    mensaje: AlertMessage.cargado
                }])
                console.log(d.response)
                let tmp = d.response

                ApiGet("/api/SolicitudCambio/Aprobadas")
                    .then(d => {
                        alertContext.setAlertas((al) => [...al, {
                            Alerta: SuccessAlert,
                            mensaje: AlertMessage.cargado
                        }])
                        console.log(d.response)
                        setAprobadas(d.response)
                        d.response.map((solicitud) => {
                            let item = tmp.find((item) => item.id == solicitud.id)
                            item.inVisible = false
                            //console.log(item)
                            //tmp.splice(tmp.findIndex((item) => item.id == solicitud.id), 1)
                        })
                        setData(d.response.concat(tmp))
                        setLoading(false)
                    })
                    .catch(d => {
                        alertContext.setAlertas((al) => [...al, {
                            Alerta: ErrorAlert,
                            mensaje: AlertMessage.noCargado
                        }])
                        console.log(d)
                        setLoading(false)
                    })
            })
            .catch(d => {
                alertContext.setAlertas((al) => [...al, {
                    Alerta: ErrorAlert,
                    mensaje: AlertMessage.noCargado
                }])
                console.log(d)
                setLoading(false)
            })
    }

    //useEffect(() => {
    //    if (data.length > 0) {
    //        let newData = data
    //        //newData.map((turno) => {

    //        //})
    //        console.log(newData)
    //    }
    //}, [data])

    useEffect(() => {
        solicitarData();
    }, [])

    useEffect(() => {
        console.log("data")
        console.log(data)
    }, [data])

    return (
        <div>
            <FullCalendar
                plugins={[dayGridPlugin, timeGridPlugin, multiMonthPlugin, interactionPlugin, rrulePlugin, momentPlugin]}
                headerToolbar={{
                    left: 'prev,next today',
                    center: 'title',
                    right: 'multiMonthYear,dayGridMonth,timeGridWeek,timeGridDay'
                }}
                views={{
                    week: {
                        dayHeaderFormat: {
                            weekday: "short",
                            day: "2-digit"
                        }
                    }
                }}
                slotLabelFormat={{
                    hour: 'numeric',
                    minute: '2-digit',
                    meridiem: 'short',
                    hour12: true
                }}
                slotLabelContent={info => {
                    if (info.text === '0:00 a. m.') {
                        info.text = '12:00 a. m.';
                    } else if (info.text === '0:00 p. m.') {
                        info.text = '12:00 p. m.';
                    }
                    return info.text
                }}
                events={data}
                eventDidMount={(info) => {
                    if (info.event.extendedProps.inVisible) {
                    }
                    let end = info.event.end
                    if (end) {

                        let timeDiffStr = end.getFullYear() + "-" + (((end.getMonth() + 1) < 10) ? "0" + (end.getMonth() + 1) : end.getMonth() + 1) + "-" + ((end.getDate() < 10) ? "0" + end.getDate() : end.getDate()) + "T"
                        if ([0, 6].includes(info.event.start.getDay())) {
                            timeDiffStr += info.event.extendedProps.cantHorasEnFinDeSemana
                        } else {
                            timeDiffStr += info.event.extendedProps.cantHorasEnDiaDeSemana
                        }
                        let timeDiff = new Date(timeDiffStr)
                        end.setTime(info.event.start.getTime() + (timeDiff.getHours() * 60 * 60 * 1000) + (timeDiff.getMinutes() * 60 * 1000) + (timeDiff.getSeconds() * 1000) + timeDiff.getMilliseconds())
                        info.event.setEnd(end)
                    }
                }}
                filterResourcesWithEvents
                locale={esLocale}
                navLinks
                height="calc(100vh - 58.4px - 2rem)"
                selectable
                nowIndicator
                now={new Date()}
                initialView="dayGridMonth"
            />
        </div>
    )
}

export default Calendario