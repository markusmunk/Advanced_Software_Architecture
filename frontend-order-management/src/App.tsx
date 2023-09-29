import { useState } from 'react'
import reactLogo from './assets/react.svg'
import BurbLogo from './assets/burp_logo.png'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div>
        <img src={BurbLogo} className="logo" alt="Burb logo" />
      </div>
      <h1>Burp 4.0</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
      </div>
    </>
  )
}

export default App
