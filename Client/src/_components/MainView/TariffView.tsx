import * as React from "react";
import style from "./tarifview.module.less"
import { СanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, ITariffs, GamerRank, DLang } from "../../_services";
import { MaterialSelect, Item } from "../Input/SelectList";
import { BaseReactComp } from "../BaseReactComponent";

interface ITarifViewProps extends React.Props<any> {
    tariff: ITariffs;
    isLoading?: boolean;
    currentRole?: GamerRank;
    [id: string]: any;
}
interface ITarifViewState {
    currentRole?: string;
}
/// Плашка с информацией о тарифе
export class TariffView extends BaseReactComp<ITarifViewProps, ITarifViewState> {
    /**
     *
     */
    constructor(props: ITarifViewProps) {
        super(props);
        this.state = {
            currentRole: props.currentRole || Object.keys(props.tariff)[0]
        }
    }

    renderRoleList = () => {
        const { tariff, currentRole } = this.props;
        const roles = Object.keys(tariff).map(t => new Item(t,  DLang('USER_ROLE', t)));
        var role = this.state.currentRole || currentRole || roles[0].key;
        if (role != this.state.currentRole)
            this.setState({ currentRole: role });
        return (
            <MaterialSelect
                items={roles}
                value={role}
                path="currentRole"
                onChange={this.onInput}
                className={style['select']}
                type='white'
            ></MaterialSelect>
        );
    }

    render() {
        const role = this.state.currentRole || '';
        const { tariff } = this.props;
        return (<СanvasBlock
            isLoading={this.props.isLoading}
            title={Lang("TARIFF_TITLE")}
            type="success"
            subChildren={this.renderRoleList()}>
            <Grid
                direction="vertical"
            >
                <Col1>
                    <NamedValue name={Lang("TARIFF_TAX")}>
                        {tariff[role].tax.join(", ") || 0}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("TARIFF_LOAN_TAX")}>
                        {tariff[role].loanTax || 0}%
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("TARIFF_LOAN_EXPTAX")}>
                        {tariff[role].expiredLoanTax || 0}%
                    </NamedValue>
                </Col1>
            </Grid>
        </СanvasBlock>
        );
    }
}