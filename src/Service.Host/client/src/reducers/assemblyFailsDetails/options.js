import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { assemblyFailsDetailsActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.assemblyFailsDetails.actionType,
    actionTypes,
    defaultState
);
