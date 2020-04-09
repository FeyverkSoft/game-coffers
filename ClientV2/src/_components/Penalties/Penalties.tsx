import * as React from "react";
import style from "./penalties.module.scss";
import { PlusCircleFilled } from '@ant-design/icons';
import { Private } from "../Private";
import { ILoanView, IPenaltyView } from "../../_services/gamer/GamersListView";
import { Tooltip, Button } from "antd";
import { Lang } from "../../_services";
import { Dictionary } from "../../core";
import { formatDateTime } from "../../_helpers";

interface IPenalties {
    penalties: Dictionary<IPenaltyView>;
    userId: string;
    onAddLoan(userId: string): void;
}
export const Penalties = ({ ...props }: IPenalties) => <div className={style['penalty_wrapper']}>
    {
        Object.keys(props.penalties).filter(_ => _ !== 'holding').map(_ => <Loan
            key={props.penalties[_].id}
            penaly={props.penalties[_]}
        />)
    }
    <Private roles={['admin', 'leader', 'officer']}>
        <Tooltip title={Lang('NEW_LOAN_MODAL')}>
            <Button
                type="link"
                className={style['add']} 
                icon={<PlusCircleFilled />}
                onClick={() => props.onAddLoan(props.userId)}
            />
        </Tooltip>
    </Private>
</div>;

interface IPenalyProps {
    penaly: IPenaltyView;
}

export const Loan = React.memo(({ ...props }: IPenalyProps) => {
    const { penaly } = props;
    return <div key={penaly.id} className={`${style['penalty']}`}>
        <Tooltip
            className={`${style['penalty']} ${style[penaly.penaltyStatus.toLowerCase()]}`}
            title={`${formatDateTime(penaly.createDate, 'd')} ${penaly.description}`}
        // onClick={() => props.showLoanInfo(loan.id, loan.userId)}
        >
            {penaly.amount}
        </Tooltip>
    </div>
});