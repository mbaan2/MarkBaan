import './App.css';
import Pressure from './Pressure/Pressure';
import ReactorControls from './ReactorControls/ReactorControls';
import ValveStatus from './ValveStatus/ValveStatus';


function App() {
    return (
        <>
            <h1>Reactor Pressure overview</h1>
            <Pressure />
            <ValveStatus />
            <ReactorControls />
        </>
    );
}

export default App;