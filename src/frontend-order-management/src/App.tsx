import { useState } from 'react'
import BurbLogo from './assets/burp_logo.png'
import './App.css'
import {MenuItem} from './components/MenuItem'

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
        <MenuItem name={'burger'} />
      </div>
    </>
  )
}

export default App
