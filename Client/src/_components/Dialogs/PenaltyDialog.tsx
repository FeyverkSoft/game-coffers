
import * as React from "react";
import { BaseReactComp } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang, IPenaltyView } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid, IStore } from "../../_helpers";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    penalty: IPenaltyView;
    onClose: Function;
    [id: string]: any;
}


class _PenaltyDialog extends BaseReactComp<IProps> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            id: getGuid(),
            description: { value: undefined },
            borrowDate: { value: new Date() },
            expiredDate: { value: new Date() },
            amount: { value: 0 },
            isLoad: false
        }
    }

    onClose = () => {
        this.props.onClose();
    }

    render() {
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('SHOW_PENALTY_MODAL')}
                onCancel={() => this.onClose()}
            >

            </Dialog>
        );
    }
}

interface _IProps extends React.Props<any> {
    isDisplayed: boolean;
    penaltyId: string;
    onClose: Function;
    [id: string]: any;
}
const connected_PenaltyDialog = connect<{}, {}, _IProps, IStore>((store, props): IProps => {
    return {
        isDisplayed: props.isDisplayed,
        onClose: props.onClose,
        penalty: props.isDisplayed ? store.gamers.gamersList[props.gamerId].penalties[props.penaltyId] : {} as IPenaltyView
    };
})(_PenaltyDialog);

export { connected_PenaltyDialog as ShowPenaltyDialog }; 