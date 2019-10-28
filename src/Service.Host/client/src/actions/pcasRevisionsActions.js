import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { pcasRevisionsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.pcasRevisions.item,
    itemTypes.pcasRevisions.actionType,
    itemTypes.pcasRevisions.uri,
    actionTypes,
    config.appRoot
);
