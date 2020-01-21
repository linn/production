import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { delPerfDetailsActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.delPerfDetails.actionType,
    actionTypes,
    defaultState
);