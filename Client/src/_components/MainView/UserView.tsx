import * as React from "react";
import { СanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, IGamerInfo, ITariffs, LangF } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";

interface IUserViewProps extends React.Props<any> {
    user: IGamerInfo;
    isLoading?: boolean;
    tax: Array<number>;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export class UserView extends BaseReactComp<IUserViewProps> {

    PinaltyRender = (): React.ReactNode => {
        const { user } = this.props;
        if (user.activePenaltyAmount == 0)
            return undefined;
        return LangF("USER_PENALTY_AMOUNT", user.activePenaltyAmount.toString());
    }

    render() {
        const { user } = this.props;
        let index: number = user.charCount - 1;
        let tax = this.props.tax[index >= 0 ? index : 0] || this.props.tax[this.props.tax.length - 1] || 0;
        return (<СanvasBlock
            isLoading={this.props.isLoading}
            title={Lang("USER_TITLE")}
            subChildren={this.PinaltyRender()}
            type={user.activePenaltyAmount <= 0 ? "success" : "error"}
            subType="error"
        >
            <Grid
                direction="vertical"
            >
                <Col1>
                    <NamedValue name={Lang("USER_BALANCE")}>
                        {user.balance}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("USER_TAX_AMOUNT")}>
                        {user.charCount * tax}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("USER_LOAN_AMOUNT")}>
                        {user.activeLoanAmount}
                    </NamedValue>
                </Col1>
            </Grid>
        </СanvasBlock>
        );
    }
}