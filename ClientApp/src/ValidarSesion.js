import { useContext, useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { EvaluarSesion } from './Api';
import { LoadContext, UserContext } from './Contexts';

const ValidarSesion = ({ children, setIsSesionValida }) => {
    const userContext = useContext(UserContext)
    const loadContext = useContext(LoadContext)
    const navigate = useNavigate()
    const location = useLocation()

    useEffect(() => {
        setIsSesionValida(EvaluarSesion(navigate, location, userContext))
    }, [location])

    useEffect(() => {
        setIsSesionValida(EvaluarSesion(navigate, location, userContext))
    }, [])

    useEffect(() => {
        if (!loadContext.isLoading) {
            if (!userContext.isSesionValida) {
                if (location.pathname.toLowerCase() !== "/login") {
                    navigate("/login", {
                        state: {
                            from: location
                        }
                    })
                }
            } else if (location.pathname.toLowerCase() === "/login") {
                navigate("/", {
                    state: {
                        from: location
                    }
                })
            }
        } else {
            loadContext.setIsLoading(false)
        }
    }, [loadContext.isLoading, userContext.isSesionValida])


    return (
        <>
            {children}
        </>
    )
}

export default ValidarSesion