import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ValveStatus = () => {
    const [valveStatus, setValveStatus] = useState("Closed");

    useEffect(() => {
        const intervalId = setInterval(() => {
            axios.get('/api/reactor/GetValveStatus')
                .then(response => {
                    setValveStatus(response.data);
                });
        }, 1000);

        return () => clearInterval(intervalId);
    }, []);

    return (
        <>
            <h2>Valvestatus: {valveStatus}</h2>
        </>
    );
};

export default ValveStatus;
