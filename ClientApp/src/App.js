import React, { createContext, useContext, useEffect, useState } from 'react';
import './custom.css';
import { AlertContextProvider, LoadContextProvider, UserContextProvider } from './Contexts';
import { SuccessAlert } from './components/Alertas';
import AppContent from './AppContent';

const App = () => {
    const [usuarioEmpleado, setUsuarioEmpleado] = useState({})
    const [isLoading, setIsLoading] = useState(true)
    const [isSesionValida, setIsSesionValida] = useState(false)
    const [alertas, setAlertas] = useState([])

    return (
        <AlertContextProvider
            alertas={alertas}
            setAlertas={setAlertas}
        >
            <LoadContextProvider
                isLoading={isLoading}
                setIsLoading={setIsLoading}
            >
                <UserContextProvider
                    isSesionValida={isSesionValida}
                    usuarioEmpleado={usuarioEmpleado}
                    setUsuarioEmpleado={setUsuarioEmpleado}
                >
                    <AppContent setIsSesionValida={setIsSesionValida} />
                </UserContextProvider>
            </LoadContextProvider>
        </AlertContextProvider>
    )
}
export default App