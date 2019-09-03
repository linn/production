import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { assemblyFailsMeasuresActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.assemblyFailsMeasures.actionType,
    actionTypes,
    defaultState
);
