import * as React from "react";
import style from "./tariffview.module.less"
import { CanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, ITariffs, GamerRank, DLang } from "../../_services";
import { Item, BaseSelect } from "../Input/SelectList";
import { BaseReactComp } from "../BaseReactComponent";

interface ITariffViewProps extends React.Props<any> {
    tariff: ITariffs;
    isLoading?: boolean;
    currentRole: GamerRank;
    [id: string]: any;
}
interface ITarifViewState {
    currentRole: string;
}
/// Плашка с информацией о тарифе
class _TariffView extends BaseReactComp<ITariffViewProps, ITarifViewState> {
    /**
     *
     */
    constructor(props: ITariffViewProps) {
        super(props);
        this.state = {
            currentRole: props.currentRole || Object.keys(props.tariff)[0]
        }
    }

    componentWillReceiveProps(props: ITariffViewProps) {
        this.setState({ currentRole: props.currentRole });
    }

    renderRoleList = () => {
        const { tariff, currentRole } = this.props;
        const roles = Object.keys(tariff).map(t => new Item(t, DLang('USER_ROLE', t)));
        var role = this.state.currentRole || currentRole || roles[0].key;
        if (role != this.state.currentRole)
            this.setState({ currentRole: role });
        return (
            <BaseSelect
                items={roles}
                value={role}
                path="currentRole"
                onChange={this.onInput}
                className={style['select']}
                type='white'
            ></BaseSelect>
        );
    }

    render() {
        const role = this.state.currentRole || '';
        const { tariff } = this.props;
        return (<CanvasBlock
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
        </CanvasBlock>
        );
    }
}

export const TariffView = React.memo(({ ...props }: ITariffViewProps) => <_TariffView {...props} />)