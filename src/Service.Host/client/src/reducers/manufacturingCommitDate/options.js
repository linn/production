import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { manufacturingCommitDateActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { date: '' };

export default reportOptionsFactory(
    reportTypes.manufacturingCommitDate.actionType,
    actionTypes,
    defaultState
);
