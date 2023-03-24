import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch } from "../../app/store/configureStore";
import { setBasket } from "../basket/basketSlice";
import CheckoutPage from "./CheckoutPage";

const stripePromise = loadStripe('pk_test_51MoshdJoMiPHbmQX8vj3HLGtM8kQhpydfRhUIvkpMj5M7WeY60dUZWJ8cOp4kQHEpTw2QYR4ATJt7DZEDsBegwek00VueGcoft');

export default function CheckoutWrapper() {
    const dispatch = useAppDispatch();
    const[loading, setLoading] = useState(true);

    useEffect(() => {
        agent.Payments.createPaymentIntent()
            .then(basket => dispatch(setBasket(basket)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [dispatch]);

    if(loading) return <LoadingComponent message="Loading checkout..."/>

    return (
        <Elements stripe={stripePromise}>
            <CheckoutPage />
        </Elements>
    )
}