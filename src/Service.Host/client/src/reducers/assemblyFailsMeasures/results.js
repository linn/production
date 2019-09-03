import { reportResultsFactory } from '@linn-it/linn-form-components-library';
import { assemblyFailsMeasuresActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { loading: false, data: null };

export default reportResultsFactory(
    reportTypes.assemblyFailsMeasures.actionType,
    actionTypes,
    defaultState
);
