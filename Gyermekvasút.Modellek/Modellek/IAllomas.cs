using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Gyermekvasút.Hálózat
{
    [ServiceKnownType(typeof(Vonat))]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "Hálózat.IAllomas")]
    public interface IAllomas
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/VonatElkuld", ReplyAction = "http://tempuri.org/IAllomas/VonatElkuldResponse")]
        void VonatElkuld(Gyermekvasút.Vonat vonat);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/VonatElkuld", ReplyAction = "http://tempuri.org/IAllomas/VonatElkuldResponse")]
        System.IAsyncResult BeginVonatElkuld(Gyermekvasút.Vonat vonat, System.AsyncCallback callback, object asyncState);

        void EndVonatElkuld(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/VonatFogad", ReplyAction = "http://tempuri.org/IAllomas/VonatFogadResponse")]
        void VonatFogad(Gyermekvasút.Vonat vonat);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/VonatFogad", ReplyAction = "http://tempuri.org/IAllomas/VonatFogadResponse")]
        System.IAsyncResult BeginVonatFogad(Gyermekvasút.Vonat vonat, System.AsyncCallback callback, object asyncState);

        void EndVonatFogad(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/OnMegcsengettek", ReplyAction = "http://tempuri.org/IAllomas/OnMegcsengettekResponse")]
        void OnMegcsengettek(bool kpFeleHiv);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/OnMegcsengettek", ReplyAction = "http://tempuri.org/IAllomas/OnMegcsengettekResponse")]
        System.IAsyncResult BeginOnMegcsengettek(bool kpFeleHiv, System.AsyncCallback callback, object asyncState);

        void EndOnMegcsengettek(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/OnVisszacsengettek", ReplyAction = "http://tempuri.org/IAllomas/OnVisszacsengettekResponse")]
        void OnVisszacsengettek(bool kpFeleHiv);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/OnVisszacsengettek", ReplyAction = "http://tempuri.org/IAllomas/OnVisszacsengettekResponse")]
        System.IAsyncResult BeginOnVisszacsengettek(bool kpFeleHiv, System.AsyncCallback callback, object asyncState);

        void EndOnVisszacsengettek(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/OnKozlemeny", ReplyAction = "http://tempuri.org/IAllomas/OnKozlemenyResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Gyermekvasút.Vonat))]
        void OnKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/OnKozlemeny", ReplyAction = "http://tempuri.org/IAllomas/OnKozlemenyResponse")]
        System.IAsyncResult BeginOnKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters, System.AsyncCallback callback, object asyncState);

        void EndOnKozlemeny(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/OnHivasMegszakitva", ReplyAction = "http://tempuri.org/IAllomas/OnHivasMegszakitvaResponse")]
        void OnHivasMegszakitva(bool kpFeleHiv);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/OnHivasMegszakitva", ReplyAction = "http://tempuri.org/IAllomas/OnHivasMegszakitvaResponse")]
        System.IAsyncResult BeginOnHivasMegszakitva(bool kpFeleHiv, System.AsyncCallback callback, object asyncState);

        void EndOnHivasMegszakitva(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAllomas/Ping", ReplyAction = "http://tempuri.org/IAllomas/PingResponse")]
        void Ping(bool kpFelePingel);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAllomas/Ping", ReplyAction = "http://tempuri.org/IAllomas/PingResponse")]
        System.IAsyncResult BeginPing(bool kpFelePingel, System.AsyncCallback callback, object asyncState);

        void EndPing(System.IAsyncResult result);

        event TelCsengetesDelegate Megcsengettek;
        event TelCsengetesDelegate Visszacsengettek;
        event TelKozlemenyDelegate Kozlemeny;
        event TelMegszakitvaDelegate HivasMegszakitva;
    }
}
