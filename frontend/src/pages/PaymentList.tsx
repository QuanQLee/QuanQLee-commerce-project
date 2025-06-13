import { useEffect, useState } from 'react'
import { Container, Typography, Card, CardContent } from '@mui/material'
import api from '../api/api'

interface Payment {
  id: string
  amount: number
  status: string
}

export default function PaymentList() {
  const [payments, setPayments] = useState<Payment[]>([])

  useEffect(() => {
    api.get('/api/v1/payment/v1/payment')
      .then(res => setPayments(res.data))
      .catch(err => console.error(err))
  }, [])

  return (
    <Container>
      <Typography variant="h4" gutterBottom>Payments</Typography>
      {payments.map(p => (
        <Card key={p.id} sx={{ mb: 2 }}>
          <CardContent>
            <Typography variant="h6">Payment #{p.id}</Typography>
            <Typography>${p.amount} - {p.status}</Typography>
          </CardContent>
        </Card>
      ))}
    </Container>
  )
}
